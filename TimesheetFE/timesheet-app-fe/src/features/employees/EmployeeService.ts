import axios from "axios";
import { Employee } from "./models/Employee";

const baseURL = "https://localhost:7048/api/employees";

export function getPage(pageSize: number = 3, pageNumber: number = 1) {
    return axios.get(baseURL + "?pageSize=" + pageSize.toString + "&PageNumber=" + pageNumber.toString);
}

export function create(employee: Employee) {
    return axios.post(baseURL, employee);
}

export function edit(employee: Employee) {
    return axios.put(baseURL, employee);
}

export function deleteEmployee(employeeId: string) {
    return axios.delete(baseURL + "/" + employeeId);
}

export function getAllEmployees() {
    return axios.get(baseURL + "/all");
}


