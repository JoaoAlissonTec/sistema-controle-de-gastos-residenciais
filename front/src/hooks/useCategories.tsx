import { useQuery } from "@tanstack/react-query";
import {Categories} from "../services/categories";

export default function useCategories(page = 1, pageSize = 20) {

    const { getAll } = Categories()

    const {data, isPending, error} = useQuery({
        queryKey: ['categories', page], 
        queryFn: () => getAll(page, pageSize),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}