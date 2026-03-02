import { Categories } from "../services/categories";
import { useQuery } from "@tanstack/react-query";

export default function useCategory(id:string) {

    const {getById} = Categories()

    const {data, isPending, error} = useQuery({
        queryKey: ['categories', id], 
        queryFn: () => getById(id),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}