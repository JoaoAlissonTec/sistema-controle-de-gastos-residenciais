import { Persons } from "../services/persons";
import { useQuery } from "@tanstack/react-query";

export default function usePerson(id:string) {

    const personsAPI = Persons()

    const {data, isPending, error} = useQuery({
        queryKey: ['persons', id], 
        queryFn: () => personsAPI.getById(id),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}