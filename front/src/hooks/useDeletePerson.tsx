import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Persons } from "../services/persons";

export default function useDeletePerson() {
    const queryClient = useQueryClient();
    const {deletePerson} = Persons();

    const {isPending, isSuccess, isError, mutate} = useMutation({
        mutationFn: (id: string) => {
            return deletePerson(id)
        },
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ["persons"]})
        }
    })

    return {isPending, isSuccess, isError, mutate}
}