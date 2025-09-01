import React, { useEffect, useState } from 'react';
import { Film, Plus, Search } from 'lucide-react';
import { getMovies, markWatched, deleteMovie, createMovie } from "../api/moviesApi";
import { Genre } from '../types/genre';
import type { Movie } from "../types/movie";
import MovieCard from "../components/MovieCard";
import AddMovieForm from "../components/addMovieForm";
import './HomePage.css'; 

const getGenreString = (genre: number): string => {
  return Genre[genre];
};

export default function HomePage() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [filteredMovies, setFilteredMovies] = useState<Movie[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterStatus, setFilterStatus] = useState<'all' | 'watched' | 'unwatched'>('all');
  const [showAddForm, setShowAddForm] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const loadMovies = async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await getMovies();
      setMovies(response.data);
    } catch (err) {
      setError('Failed to load movies. Please check if your API server is running.');
      console.error('Error loading movies:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleWatch = async (id: string, isWatched: boolean) => {
    try {
      await markWatched(id, isWatched);
      await loadMovies();
    } catch (err) {
      setError('Failed to update watch status');
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await deleteMovie(id);
      await loadMovies();
    } catch (err) {
      setError('Failed to delete movie');
    }
  };

  const handleAddMovie = async (movieData: Partial<Movie>) => {
    try {
      await createMovie(movieData);
      await loadMovies();
    } catch (err) {
      setError('Failed to add movie');
    }
  };

  useEffect(() => {
    loadMovies();
  }, []);

  useEffect(() => {
    let filtered = movies;
    
    if (searchTerm) {
      filtered = filtered.filter(movie =>
        movie.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
        getGenreString(movie.genre).toLowerCase().includes(searchTerm.toLowerCase()) ||
        movie.director?.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }
    
    if (filterStatus !== 'all') {
      filtered = filtered.filter(movie =>
        filterStatus === 'watched' ? movie.isWatched : !movie.isWatched
      );
    }
    
    setFilteredMovies(filtered);
  }, [movies, searchTerm, filterStatus]);

  if (loading) {
    return (
      <div className="loading-container">
        <div className="loading-text">
          <div className="loading-spinner"></div>
          <p>Loading movies...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="container">
      {/* Header */}
      <div className="header">
        <div className="header-inner">
          <div className="header-title">
            <Film size={28} style={{ color: '#007bff' }} />
            <h1>Movie Catalog</h1>
          </div>
          <button
            onClick={() => setShowAddForm(true)}
            className="add-movie-button"
          >
            <Plus size={16} />
            Add Movie
          </button>
        </div>
      </div>

      <div className="movies-container">
        {/* Error */}
        {error && (
          <div className="error-message">
            {error}
            <button 
              onClick={() => setError(null)} 
              className="error-close-button"
            >
              ×
            </button>
          </div>
        )}

        {/* Search */}
        <div className="search-section">
          <div className="search-container">
            <div className="search-input-container">
              <Search size={16} className="search-icon" />
              <input
                type="text"
                placeholder="Search movies..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="search-input"
              />
            </div>
            
            <select
              value={filterStatus}
              onChange={(e) => setFilterStatus(e.target.value as 'all' | 'watched' | 'unwatched')}
              className="filter-select"
            >
              <option value="all">All Movies</option>
              <option value="watched">Watched</option>
              <option value="unwatched">Not Watched</option>
            </select>
          </div>
        </div>

        {/* Movies */}
        {filteredMovies.length === 0 ? (
          <div className="no-movies">
            <Film size={48} className="no-movies-icon" />
            <h3 className="no-movies-text">
              {searchTerm || filterStatus !== 'all' ? 'No movies found' : 'No movies yet'}
            </h3>
            <p className="no-movies-description">
              {searchTerm || filterStatus !== 'all' 
                ? 'Try different search terms or filters' 
                : 'Add your first movie to get started'
              }
            </p>
            {!searchTerm && filterStatus === 'all' && (
              <button
                onClick={() => setShowAddForm(true)}
                className="add-first-movie-button"
              >
                Add Your First Movie
              </button>
            )}
          </div>
        ) : (
          <div className="movies-list">
            {filteredMovies.map((movie) => (
              <MovieCard
                key={movie.id}
                movie={{ ...movie, genre: getGenreString(movie.genre) }}
                onWatch={(isWatched) => handleWatch(movie.id, isWatched)}
                onDelete={() => handleDelete(movie.id)}
              />
            ))}
          </div>
        )}
      </div>

      {showAddForm && (
        <AddMovieForm
          onAdd={handleAddMovie}
          onCancel={() => setShowAddForm(false)}
        />
      )}
    </div>
  );
}
