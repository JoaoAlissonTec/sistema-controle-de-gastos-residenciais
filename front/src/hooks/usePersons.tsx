import Persons from "../services/persons";
import { useQuery } from "@tanstack/react-query";

export default function usePersons(page = 1) {

    const personsAPI = Persons()

    const {data, isPending, error} = useQuery({
        queryKey: ['persons', page], 
        queryFn: () => personsAPI.getAll(page),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}