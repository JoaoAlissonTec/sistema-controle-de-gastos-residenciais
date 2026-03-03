import { useEffect, useState } from "react"
import useTransaction from "../../hooks/useTransactions";
import Pagination from "../../components/Pagination";
import { useNavigate } from "react-router";
import Button from "../../components/Button";

export default function Transactions() {

    const types: Record<number, string> = {
        0: "Receita",
        1: "Despesa"
    }

    const [page, setPage] = useState<number>(1);
    const { data, isPending, error } = useTransaction(page);
    const navigate = useNavigate();

    useEffect(() => {
        document.title = 'Transações';
    }, [])

    if (isPending) return <p>Loading...</p>;
    if (error) return <p>{error.message}</p>;

    return (
        <div className="p-3">
            <div className="p-4 bg-white rounded-lg">
                <div className="flex items-end justify-between">
                    <div>
                        <h1 className="text-lg font-bold">Transações</h1>
                        <p className="text-sm text-gray-400">Transações realizadas:</p>
                    </div>
                    <Button onClick={() => navigate("new")}>Nova transação</Button>
                </div>
                <div className="p-2">
                    <table className="w-full text-sm">
                        <thead className="border-b border-gray-300 text-start text-gray-400">
                            <tr>
                                <th className="text-start uppercase py-4">Descrição</th>
                                <th className="text-start uppercase">Valor</th>
                                <th className="text-start uppercase">Tipo</th>
                                <th className="text-start uppercase">Pessoa</th>
                                <th className="text-start uppercase">Categoria</th>
                            </tr>
                        </thead>
                        <tbody>
                            {data?.data?.map((transaction) => (
                                <tr key={transaction.id} className="border-b border-gray-300">
                                    <td className="py-4">{transaction.description}</td>
                                    <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(transaction.amount)}</td>
                                    <td>{types[transaction.type]}</td>
                                    <td className="underline cursor-pointer" onClick={() => navigate(`/persons/${transaction.person.id}`)}>{transaction.person.name}</td>
                                    <td className="underline cursor-pointer" onClick={() => navigate(`/categories/${transaction.category.id}`)}>{transaction.category.description}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <Pagination selectedPage={page} totalPages={data?.totalPages ?? 0} fnChangePage={setPage} />
            </div>
        </div>
    )
}