import { api } from "../config/axios";
import type { PagedResultWithTotals } from "../interfaces/PagedResult";
import type { Transaction } from "./transactions";

export interface Category {
    id?: string,
    description: string,
    type: number,
    transactions?: Transaction[]
}

interface CategoryWithTotals extends Category {
    totalIncome: number,
    totalExpense: number,
    balance: number
}

export function Categories() {
    async function getAll(page = 1, pageSize = 20): Promise<PagedResultWithTotals<CategoryWithTotals[]> | undefined> {
        try {
            const response = await api.get(`Categories/TotalTransactions?page=${page}&pageSize=${pageSize}`);
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    async function getById(id: string): Promise<Category | undefined> {
        try {
            const response = await api.get(`Categories/${id}`);
            const data = response.data;

            return data;
        } catch(err) {
            throw err;
        }
    }

    async function add(category: Category) {
        try {
            const response = await api.post("Categories", category);
            const data = response.data;

            return data;
        }catch(err) {
            throw err;
        }
    }

    return {getAll, getById, add}
}