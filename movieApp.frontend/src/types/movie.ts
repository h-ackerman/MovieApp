export interface Movie {
    id: string;
    title: string;
    description?: string;
    releaseYear: number;
    genre: number;
    director?: string;
    runtimeMinutes?: number;
    myRating?: number;
    isWatched: boolean;
    
  }
  