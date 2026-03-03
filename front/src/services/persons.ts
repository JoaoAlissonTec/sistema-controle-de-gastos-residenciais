import { api } from "../config/axios";
import type { PagedResultWithTotals } from "../interfaces/PagedResult";
import type { Transaction } from "./transactions";

export interface Person {
    id?: string,
    name: string,
    age: number,
    transactions?: Transaction[]
}

interface PersonWithTotals extends Person {
    totalIncome: number, 
    totalExpense: number,
    balance: number
}

export function Persons() {
    async function getAll(page = 1, pageSize = 20): Promise<PagedResultWithTotals<PersonWithTotals[]> | undefined> {
        try{
            const response = await api.get(`/Persons/TotalTransactions?page=${page}&pageSize=${pageSize}`)
            const data = response.data;

            return data
        }catch(err){
            throw err;
        }
    }

    async function getById(id: string): Promise<Person | undefined> {
        try{
            const response = await api.get(`/Persons/${id}`)
            const data = response.data;

            return data
        }catch(err){
            throw err;
        }
    }

    async function add(person: Person): Promise<Person | undefined> {
        try {
            const response = await api.post("/Persons", person);
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    async function update(person: Person): Promise<Person | undefined> {
        try {
            const response = await api.put("/Persons", person);
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    async function deletePerson(id: string) {
        try {
            await api.delete(`/Persons/${id}`)
        }catch(err) {
            throw err;
        }
    }

    return {getAll, getById, add, update, deletePerson}
}