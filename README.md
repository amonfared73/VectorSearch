# GloVe Word Search Application

This repository contains a C# WPF application designed for searching over four pre-trained GloVe dictionaries. The application provides two search approaches: **Simple Search** and **Vector Search**, enabling efficient and intelligent exploration of word embeddings.

## Features

### GloVe Dictionaries
The application supports the following GloVe pre-trained models:
- **glove_6B_50d**: 50 dimensions
- **glove_6B_100d**: 100 dimensions
- **glove_6B_200d**: 200 dimensions
- **glove_6B_300d**: 300 dimensions

### Search Approaches
1. **Simple Search**: Uses a `LIKE` query to match words similar to the given input.
2. **Vector Search**: Finds words with similar meanings based on cosine similarity of word vectors.

## Architecture
The application is built with a clean architecture design, consisting of four layers:

1. **Domain Layer**:
   - Contains the core business logic.
   - Defines the `Word` entity with properties:
     - `Text`: The word string.
     - `Vector`: A double array representing the word embedding.
     - `DictionaryTypeId`: Refers to the specific GloVe dictionary.

2. **Application Service Layer**:
   - Contains services for executing search operations.
   - Implements logic for simple and vector search queries.

3. **Entity Framework Layer**:
   - Acts as the data access layer.
   - Interacts with the SQL Server database using Entity Framework.
   - Stores GloVe word embeddings in the `dbo.Words` table with columns:
     - `Id`: Unique identifier.
     - `Text`: Word string.
     - `Vector`: Word vector serialized as `nvarchar(max)`.
     - `DictionaryTypeId`: GloVe dictionary identifier.

4. **WPF (UI) Layer**:
   - Provides a user-friendly interface.
   - Allows users to select a GloVe dictionary and choose between simple or vector search.
   - Displays search results in a list.

## Prerequisites

- .NET Framework/SDK (compatible with WPF)
- SQL Server database
- GloVe pre-trained model files

## Installation and Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/amonfared73/VectorSearch
   ```

2. **Configure the Database**:
   - Ensure the SQL Server database is running.
   - Create a `dbo.Words` table with the appropriate schema.
   - Import GloVe data into the database. The application supports models such as `glove.6B.50d.txt`.

3. **Build and Run**:
   - Open the solution in Visual Studio.
   - Build the project to restore dependencies.
   - Run the application.

## Usage

1. Launch the application.
2. Select one of the four GloVe dictionaries.
3. Choose a search approach:
   - **Simple Search**: Enter a word or partial text to find similar matches.
   - **Vector Search**: Enter a word to retrieve words with similar meanings based on cosine similarity.
4. View results displayed in the application.

## Screenshots
![ScreenShots](VectorSearch.WPF/ScreenShots/1.png)

## Folder Structure

```
.
├── Domain                  # Core business logic and entities
├── ApplicationService      # Services for handling search operations
├── EntityFramework         # Data access and database interaction
├── WPF                     # User interface layer
└── README.md               # Project documentation
```

## Future Enhancements

- Add support for additional pre-trained word embeddings.
- Optimize vector search performance with advanced indexing techniques.
- Improve the UI/UX for better usability.

## Contributing

Contributions are welcome! Please create a pull request or open an issue to discuss improvements or bugs.


## Contact

For inquiries or support, contact amonfared73@gmail.com

