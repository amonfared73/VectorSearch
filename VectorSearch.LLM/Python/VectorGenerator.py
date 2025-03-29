from fastapi import FastAPI
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer
import joblib
import numpy as np

app = FastAPI()

model = SentenceTransformer('heydariAI/persian-embeddings')

pca = joblib.load('pca_model.pkl')  

class QueryModel(BaseModel):
    query: str

@app.post("/generate_vector/")
async def generate_vector(data: QueryModel):
    embedding_1024d = model.encode([data.query])[0]
    
    embedding_50d = pca.transform(np.array(embedding_1024d).reshape(1, -1))[0]
    
    return {"vector": embedding_50d.tolist()}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=8000)
