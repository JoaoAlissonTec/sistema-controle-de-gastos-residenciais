import { useNavigate, useParams } from "react-router"
import useCategory from "../../hooks/useCategory";
import { ChevronLeft } from "lucide-react";

export default function CategoryTransactions() {
    const types: Record<number, string> = {
        0: "Receita",
        1: "Despesa"
    }

    const navigate = useNavigate()
    const params = useParams()

    const { data, isPending, error } = useCategory(params.id ?? "")

    if (isPending) return <p>Loading...</p>;
    if (error) return <p>{error.message}</p>;

    return (
        <div className="p-3">
            <div className="p-4 bg-white rounded-lg">
                <div className="flex gap-4">
                    <button onClick={() => navigate(-1)} className="hover:bg-gray-200 px-2 rounded-lg cursor-pointer"><ChevronLeft size={16}/></button>
                    <div>
                        <h1 className="text-lg font-bold">Transações de {data?.description}</h1>
                        <p className="text-sm text-gray-400">Transações realizadas:</p>
                    </div>
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
                            {data?.transactions?.map((transaction) => (
                                <tr key={transaction.id} className="border-b border-gray-300">
                                    <td className="py-4">{transaction.description}</td>
                                    <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(transaction.amount)}</td>
                                    <td>{types[transaction.type]}</td>
                                    <td>{transaction.person.name}</td>
                                    <td>{data.description}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}