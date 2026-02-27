import axios, { type AxiosInstance } from "axios";


const baseURL: string | undefined = `${import.meta.env.APP_API_BASE_URL}/api`
console.log(import.meta.env)
const api: AxiosInstance = axios.create({baseURL: baseURL})

export {api}