import { useEffect, useState } from "react";
import Pagination from "../../components/Pagination";
import { Banknote, BanknoteArrowDown, BanknoteArrowUp } from "lucide-react";
import Card from "../../components/Card";
import { useNavigate } from "react-router";
import Button from "../../components/Button";
import useCategories from "../../hooks/useCategories";

export default function Categories() {
    const types: Record<number, string> = {
        0: "Receita",
        1: "Despesa",
        2: "Ambas"
    }

    const [page, setPage] = useState<number>(1)
    const { data, isPending, error } = useCategories(page);

    useEffect(() => {
        document.title = 'Categorias';
    }, [])

    const navigate = useNavigate()

    if (isPending) return <p>Loading...</p>;
    if (error) return <p>{error.message}</p>;

    return (
        <>
            <div className="bg-white p-4 flex gap-4">
                <Card icon={BanknoteArrowDown} label="Receita" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.totalIncome ?? 0)} />
                <Card icon={BanknoteArrowUp} label="Despesa" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.totalExpense ?? 0)} />
                <Card icon={Banknote} label="Saldo" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.balance ?? 0)} />
            </div>
            <div className="p-3">
                <div className="p-4 bg-white rounded-lg">
                    <div className="flex justify-between items-end">
                        <div>
                            <h1 className="text-lg font-bold">Categorias</h1>
                            <p className="text-sm text-gray-400">Categorias cadastradas no sistemas:</p>
                        </div>
                        <Button onClick={() => navigate("new")}>Cadastrar nova categoria</Button>
                    </div>
                    <div className="p-2">
                        <table className="w-full text-sm">
                            <thead className="border-b border-gray-300 text-start text-gray-400">
                                <tr>
                                    <th className="text-start uppercase py-4 pl-2">Descrição</th>
                                    <th className="text-start uppercase">Tipo</th>
                                    <th className="text-start uppercase">Receita</th>
                                    <th className="text-start uppercase">Despesa</th>
                                    <th className="text-start uppercase">Saldo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {data?.data?.map((category) => (
                                    <tr key={category.id} className="border-b border-gray-300 hover:bg-gray-100" onClick={() => navigate(`${category.id}`)}>
                                        <td className="py-4 pl-2">{category.description}</td>
                                        <td>{types[category.type]}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(category.totalIncome)}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(category.totalExpense)}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(category.balance)}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                    <Pagination selectedPage={page} totalPages={data?.totalPages ?? 0} fnChangePage={setPage} />
                </div>
            </div>
        </>
    );
}