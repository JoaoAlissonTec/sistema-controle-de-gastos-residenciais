import { useMutation } from "@tanstack/react-query";
import { Categories, type Category } from "../services/categories";

export default function useCreateCategory() {
    const {add} = Categories();

    const {isPending, isSuccess, isError, mutate} = useMutation({
        mutationFn: (newCategory: Category) => {
            return add(newCategory)
        }
    })

    return {isPending, isSuccess, isError, mutate}
}