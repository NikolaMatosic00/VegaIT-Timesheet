import axios from "axios";
import { Category } from "./models/Category";

const baseURL = "https://localhost:7048/api/categories";

export function getPage(firstLetter: string, name: string, pageSize: number = 3, pageNumber: number = 1) {
    return axios.get(baseURL + "?FirstLetter=" + firstLetter + "&Name=" + name + "&pageSize=" + pageSize + "&PageNumber=" + pageNumber);
}

export function create(category: Category) {
    return axios.post(baseURL, category);
}

export function edit(category: Category) {
    return axios.put(baseURL, category);
}

export function deleteCategoryByID(categoryId: string) {
    return axios.delete(baseURL + "/" + categoryId);
}


