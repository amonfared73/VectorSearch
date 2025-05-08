from fastapi import FastAPI, Query, HTTPException
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer
import joblib
import numpy as np

app = FastAPI()

model = SentenceTransformer('heydariAI/persian-embeddings') 

pca_models = {
    "digiKala": "pca_model.pkl",
    "faranShimi": "268_FaranShimi.pkl",
    "psg": "117_PSG.pkl",
}

loaded_pcas = {
    name: joblib.load(filename) for name, filename in pca_models.items()
}

class QueryModel(BaseModel):
    query: str

@app.post("/generate_vector/")
async def generate_vector(data: QueryModel, company: str = Query(...)):
    if company not in loaded_pcas:
        raise HTTPException(status_code=404, detail=f"PCA model not found for company '{company}'")

    try:
        embedding_1024d = model.encode([data.query])[0]
        pca = loaded_pcas[company]
        embedding_50d = pca.transform(np.array(embedding_1024d).reshape(1, -1))[0]
        return {"vector": embedding_50d.tolist()}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=8000)
