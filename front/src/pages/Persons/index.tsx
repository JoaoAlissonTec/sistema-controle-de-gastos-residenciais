import { useEffect, useState } from "react";
import usePersons from "../../hooks/usePersons"
import Pagination from "../../components/Pagination";
import { Banknote, BanknoteArrowDown, BanknoteArrowUp, Edit, Trash } from "lucide-react";
import Card from "../../components/Card";
import { useNavigate } from "react-router";
import Button from "../../components/Button";
import useDeletePerson from "../../hooks/useDeletePerson";
import Modal from "../../components/Modal";
import type { Person } from "../../services/persons";

export default function Persons() {
    const [page, setPage] = useState<number>(1)
    const { data, isPending, error } = usePersons(page);
    const { mutate } = useDeletePerson()

    const [personToDelete, setPersonToDelete] = useState<Person | null>(null);

    const navigate = useNavigate()

    useEffect(() => {
        document.title = 'Pessoas';
    }, [])

    if (isPending) return <p>Loading...</p>;
    if (error) return <p>{error.message}</p>;

    return (
        <>
            <div className="bg-white p-4 flex gap-4 border-b border-gray-200">
                <Card icon={BanknoteArrowDown} label="Receita" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.totalIncome ?? 0)} />
                <Card icon={BanknoteArrowUp} label="Despesa" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.totalExpense ?? 0)} />
                <Card icon={Banknote} label="Saldo" value={Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(data?.balance ?? 0)} />
            </div>
            <div className="p-3">
                <div className="p-4 bg-white rounded-lg">
                    <div className="flex justify-between items-end">
                        <div>
                            <h1 className="text-lg font-bold">Pessoas</h1>
                            <p className="text-sm text-gray-400">Pessoas cadastradas no sistemas:</p>
                        </div>
                        <Button onClick={() => navigate("new")}>Cadastrar nova pessoa</Button>
                    </div>
                    <div className="p-2">
                        <table className="w-full text-sm">
                            <thead className="border-b border-gray-300 text-start text-gray-400">
                                <tr>
                                    <th className="text-start uppercase py-4 pl-2">Nome</th>
                                    <th className="text-start uppercase">Idade</th>
                                    <th className="text-start uppercase">Receita</th>
                                    <th className="text-start uppercase">Despesa</th>
                                    <th className="text-start uppercase">Saldo</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {data?.data?.map((person) => (
                                    <tr key={person.id} className="border-b border-gray-300 hover:bg-gray-100" onClick={() => navigate(`${person.id}`)}>
                                        <td className="py-4 pl-2">{person.name}</td>
                                        <td>{person.age}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(person.totalIncome)}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(person.totalExpense)}</td>
                                        <td>{Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(person.balance)}</td>
                                        <td>
                                            <Edit size={16} className="cursor-pointer" onClick={(event) => {
                                                event.stopPropagation()
                                                navigate(`edit/${person.id}`)
                                            }} />
                                        </td>
                                        <td>
                                            <Trash size={16} className="cursor-pointer" onClick={(event) => {
                                                event.stopPropagation()
                                                setPersonToDelete(person);
                                            }} />
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                    <Pagination selectedPage={page} totalPages={data?.totalPages ?? 0} fnChangePage={setPage} />
                </div>
                <Modal
                    isOpen={personToDelete != null}
                    title={`Deletar ${personToDelete?.name}`}
                    onClose={() => setPersonToDelete(null)}
                    footer={
                        <>
                            <Button variant="secondary" onClick={() => setPersonToDelete(null)}>Cancelar</Button>
                            <Button variant="danger" onClick={() => {
                                if (personToDelete?.id)
                                    mutate(personToDelete.id)
                                setPersonToDelete(null)
                            }}>Deletar</Button>
                        </>
                    }
                >
                    Deseja realmente deletar {personToDelete?.name}?
                </Modal>
            </div>
        </>
    );
}