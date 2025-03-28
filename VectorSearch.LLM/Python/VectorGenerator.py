
from fastapi import FastAPI
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer
from sklearn.decomposition import PCA
import numpy as np

app = FastAPI()
model = SentenceTransformer('heydariAI/persian-embeddings')
pca = PCA(n_components=50) 

class QueryModel(BaseModel):
    query: str

@app.post("/generate_vector/")
async def generate_vector(data: QueryModel):
    embedding = model.encode([data.query])[0]
    reduced_vector = pca.transform(np.array([embedding]))[0]
    return {"vector": reduced_vector.tolist()}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=8000)