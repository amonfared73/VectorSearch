from fastapi import FastAPI
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer

app = FastAPI()

model = SentenceTransformer('heydariAI/persian-embeddings')

class QueryModel(BaseModel):
    query: str

@app.post("/generate_vector/")
async def generate_vector(data: QueryModel):
    embedding = model.encode([data.query])[0]
    return {"vector": embedding.tolist()}

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="127.0.0.1", port=8000)
