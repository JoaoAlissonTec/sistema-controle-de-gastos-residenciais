import { api } from "../config/axios";
import type { PagedResult } from "../interfaces/PagedResult";
import type { Category } from "./categories";
import type { Person } from "./persons";

export interface Transaction {
    id: string,
    description: string,
    amount: number,
    type: number,
    person: Person,
    category: Category
}

export interface TransactionCreate {
    description: string,
    amount: number,
    type: number,
    personId: string,
    categoryId: string
}

export function Transactions() {
    async function getAll(page = 1): Promise<PagedResult<Transaction[]> | undefined> {
        try{
            const response = await api.get(`/Transactions?page=${page}`)
            const data = response.data;

            return data
        }catch(err){
            throw err;
        }
    }

    async function getByPersonId(personId: string, page = 1): Promise<PagedResult<Transaction[]> | undefined> {
        try{
            const response = await api.get(`/Transactions/Person/${personId}?page=${page}`)
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    async function add(transaction: TransactionCreate): Promise<Transaction | undefined> {
        try {
            const response = await api.post("/Transactions", transaction);
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    return {getAll, getByPersonId, add}
}