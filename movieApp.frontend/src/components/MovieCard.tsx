import { Clock, Calendar, User, Film, Trash2, Eye, EyeOff } from 'lucide-react';
import type { Movie } from "../types/movie";
import './MovieCard.css';

interface Props {
  movie: Movie;
  onWatch: (isWatched: boolean) => void;
  onDelete: () => void;
}

export default function MovieCard({ movie, onWatch, onDelete }: Props) {
  const formatRuntime = (minutes?: number) => {
    if (!minutes) return 'Unknown';
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins}m`;
  };

  

  return (
    <div className="card">
      <div className="cardContent">
        {/* Header */}
        <div className="header">
          <div className="titleSection">
            <h2 className="title">{movie.title}</h2>
            <div className="metadata">
              <div className="metadataItem">
                <Calendar size={14} />
                <span>{movie.releaseYear}</span>
              </div>
              <div className="metadataItem">
                <Film size={14} />
                <span className="genreBadge">{movie.genre}</span>
              </div>
              {movie.runtimeMinutes && (
                <div className="metadataItem">
                  <Clock size={14} />
                  <span>{formatRuntime(movie.runtimeMinutes)}</span>
                </div>
              )}
            </div>
          </div>

          <div className={`statusBadge ${movie.isWatched ? 'watchedBadge' : 'unwatchedBadge'}`}>
            {movie.isWatched ? '✓ Watched' : '○ Not Watched'}
          </div>
        </div>

        {/* Description */}
        {movie.description && (
          <p className="description">{movie.description}</p>
        )}

        {/* Director */}
        {movie.director && (
          <div className="directorSection">
            <User size={16} className="icon" />
            <span className="directorText">
              Directed by <strong>{movie.director}</strong>
            </span>
          </div>
        )}


        

        {/* Action buttons */}
        <div className="buttonContainer">
          <button
            onClick={() => onWatch(!movie.isWatched)}
            className={`button ${movie.isWatched ? 'unwatchButton' : 'watchButton'}`}
          >
            {movie.isWatched ? <EyeOff size={16} /> : <Eye size={16} />}
            <span className="buttonText">
              {movie.isWatched ? 'Mark Unwatched' : 'Mark Watched'}
            </span>
          </button>

          <button
            onClick={onDelete}
            className="button deleteButton"
          >
            <Trash2 size={16} />
            <span className="buttonText">Delete</span>
          </button>
        </div>
      </div>
    </div>
  );
}
