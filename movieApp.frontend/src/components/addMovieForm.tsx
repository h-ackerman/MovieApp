import { useState } from 'react';
import type { Movie } from "../types/movie";
import './addMovieForm.css';

interface Props {
  onAdd: (movie: Partial<Movie>) => void;
  onCancel: () => void;
}

export default function AddMovieForm({ onAdd, onCancel }: Props) {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    releaseYear: new Date().getFullYear(),
    genre: '',
    director: '',
    runtimeMinutes: ''
  });

  const handleSubmit = () => {
    if (!formData.title.trim()) return;
    const movieData = {
      ...formData,
      runtimeMinutes: formData.runtimeMinutes ? parseInt(formData.runtimeMinutes) : undefined,
      isWatched: false
    };
    onAdd(movieData);
    onCancel();
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2 className="modal-title">Add New Movie</h2>
        <div className="form-fields">
          <div>
            <label className="input-label">Title *</label>
            <input
              type="text"
              required
              value={formData.title}
              onChange={(e) => setFormData({ ...formData, title: e.target.value })}
              className="input-field"
            />
          </div>

          <div>
            <label className="input-label">Description</label>
            <textarea
              value={formData.description}
              onChange={(e) => setFormData({ ...formData, description: e.target.value })}
              className="input-field"
              rows={3}
            />
          </div>

          <div className="input-grid">
            <div>
              <label className="input-label">Release Year *</label>
              <input
                type="number"
                required
                value={formData.releaseYear}
                onChange={(e) => setFormData({ ...formData, releaseYear: parseInt(e.target.value) })}
                className="input-field"
              />
            </div>
            
            <div>
              <label className="input-label">Runtime (min)</label>
              <input
                type="number"
                value={formData.runtimeMinutes}
                onChange={(e) => setFormData({ ...formData, runtimeMinutes: e.target.value })}
                className="input-field"
              />
            </div>
          </div>

          <div>
            <label className="input-label">Genre *</label>
            <input
              type="text"
              required
              value={formData.genre}
              onChange={(e) => setFormData({ ...formData, genre: e.target.value })}
              className="input-field"
              placeholder="e.g., Action, Drama, Comedy"
            />
          </div>

          <div>
            <label className="input-label">Director</label>
            <input
              type="text"
              value={formData.director}
              onChange={(e) => setFormData({ ...formData, director: e.target.value })}
              className="input-field"
            />
          </div>

          <div className="button-container">
            <button
              onClick={handleSubmit}
              disabled={!formData.title.trim()}
              className="primary-button"
            >
              Add Movie
            </button>
            <button
              onClick={onCancel}
              className="secondary-button"
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
