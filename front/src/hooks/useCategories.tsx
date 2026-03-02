import { useQuery } from "@tanstack/react-query";
import {Categories} from "../services/categories";

export default function useCategories(page = 1) {

    const { getAll } = Categories()

    const {data, isPending, error} = useQuery({
        queryKey: ['categories', page], 
        queryFn: () => getAll(page),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}