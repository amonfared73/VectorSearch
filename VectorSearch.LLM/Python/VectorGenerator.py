from fastapi import FastAPI
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer
import joblib
import numpy as np

app = FastAPI()

# Load the Persian sentence transformer model
model = SentenceTransformer('heydariAI/persian-embeddings')

# Load the trained PCA model
pca = joblib.load('pca_model.pkl')  # Ensure that 'pca_model.pkl' is in the correct path

class QueryModel(BaseModel):
    query: str

@app.post("/generate_vector/")
async def generate_vector(data: QueryModel):
    # Generate the 1024D embedding from the input query
    embedding_1024d = model.encode([data.query])[0]
    
    # Reduce the embedding to 50D using the PCA model
    embedding_50d = pca.transform(np.array(embedding_1024d).reshape(1, -1))[0]
    
    return {"vector": embedding_50d.tolist()}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=8000)
