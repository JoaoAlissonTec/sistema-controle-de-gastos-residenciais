import { useMutation } from "@tanstack/react-query";
import { Persons, type Person } from "../services/persons";

export default function useCreatePerson() {
    const {add} = Persons();

    const {isPending, isSuccess, isError, mutate} = useMutation({
        mutationFn: (newPerson: Person) => {
            return add(newPerson)
        }
    })

    return {isPending, isSuccess, isError, mutate}
}