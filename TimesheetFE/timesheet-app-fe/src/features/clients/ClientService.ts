import axios from "axios";
import { Client } from "./models/Client";

const baseURL = "https://localhost:7048/api/clients";

export function getPage(firstLetter: string, name: string, pageSize: number = 3, pageNumber: number = 1) {
    return axios.get(baseURL + "?FirstLetter=" + firstLetter + "&Name=" + name + "&pageSize=" + pageSize + "&PageNumber=" + pageNumber);
}

export function create(client: Client) {
    return axios.post(baseURL, client);
}

export function edit(client: Client) {
    return axios.put(baseURL, client);
}

export function deleteClientByID(clientId: string) {
    return axios.delete(baseURL + "/" + clientId);
}

export function getAllClients() {
    return axios.get(baseURL + "/all");
}


