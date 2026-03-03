import { useMutation } from "@tanstack/react-query";
import { Transactions, type TransactionCreate } from "../services/transactions";

export default function useCreateTransaction() {
    const {add} = Transactions();

    const {isPending, isSuccess, isError, mutate} = useMutation({
        mutationFn: (newTransaction: TransactionCreate) => {
            return add(newTransaction)
        }
    })

    return {isPending, isSuccess, isError, mutate}
}