import { useMutation } from "@tanstack/react-query";
import { Persons, type Person } from "../services/persons";

export default function useUpdatePerson() {
    const {update} = Persons();

    const {isPending, isSuccess, isError, mutate} = useMutation({
        mutationFn: (updatedPerson: Person) => {
            return update(updatedPerson)
        }
    })

    return {isPending, isSuccess, isError, mutate}
}