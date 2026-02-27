import { api } from "../config/axios";

interface PagedResult<T> {
    page: number,
    pageSize: number,
    totalCount: number,
    totalPages: number,
    data: T
}

interface Person {
    id: string,
    name: string,
    age: number
}

export default function Persons() {
    async function getAll(page = 1): Promise<PagedResult<Person[]> | undefined> {
        try{
            const response = await api.get(`/Persons?page=${page}`)
            const data = response.data;

            return data
        }catch(err){
            throw err;
        }
    }

    return {getAll}
}