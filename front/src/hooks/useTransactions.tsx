import { useQuery } from "@tanstack/react-query";
import { Transactions } from "../services/transactions";

export default function useTransaction(page = 1) {

    const transactionAPI = Transactions()

    const {data, isPending, error} = useQuery({
        queryKey: ['transactions', page], 
        queryFn: () => transactionAPI.getAll(page),
        placeholderData: (previousData, _) => previousData
    })

    return {data, isPending, error}
}