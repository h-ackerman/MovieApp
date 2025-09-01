import axios from "axios";
import type { Movie } from "../types/movie";

const api = axios.create({
  baseURL: "http://localhost:5000/api/Movies",
});

export const getMovies = () => api.get<Movie[]>("/");
export const getMovie = (id: string) => api.get<Movie>(`/${id}`);
export const createMovie = (movie: Partial<Movie>) => api.post("/", movie);
export const updateMovie = (id: string, movie: Partial<Movie>) => api.put(`/${id}`, movie);
export const markWatched = (id: string, isWatched: boolean) => api.patch(`/${id}/watch`, { id, isWatched });
export const deleteMovie = (id: string) => api.delete(`/${id}`);
