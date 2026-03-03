import { useEffect, useState } from "react";
import Button from "../../components/Button";
import Input from "../../components/Input";
import Select from "../../components/Select";
import type { TransactionCreate } from "../../services/transactions";
import { type Person } from "../../services/persons";
import { type Category } from "../../services/categories";
import Modal from "../../components/Modal";
import usePersons from "../../hooks/usePersons";
import Pagination from "../../components/Pagination";
import useCategories from "../../hooks/useCategories";
import useCreateTransaction from "../../hooks/useCreateTransaction";
import { useNavigate } from "react-router";

export default function NewTransaction() {
    const types: Record<number, string> = {
        0: "Receita",
        1: "Despesa",
        2: "Ambas"
    }

    const navigate = useNavigate();
    const [description, setDescription] = useState<string | undefined>();
    const [amount, setAmount] = useState<number>(0);
    const [type, setType] = useState<number>(0);
    const [person, setPerson] = useState<Person | undefined>();
    const [category, setCategory] = useState<Category | undefined>();

    const [pagePerson, setPagePerson] = useState<number>(1)
    const { data: personData, isPending: isPendingPerson } = usePersons(pagePerson, 5)

    const [pageCategory, setPageCategory] = useState<number>(1);
    const { data: categoryData, isPending: isPendingCategory } = useCategories(pageCategory, 5);

    const { mutate, isSuccess, isPending, isError } = useCreateTransaction()

    const [modalType, setModalType] = useState<"person" | "category" | null>(null);

    function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();

        if (isPending) return;
        if (!description || !person || !category) return;

        const transaction: TransactionCreate = {
            description,
            amount,
            type,
            personId: person.id!,
            categoryId: category.id!
        }

        mutate(transaction);
    }

    useEffect(() => {
        document.title = 'Nova transação';
    }, [])

    useEffect(() => {
        if (isSuccess) {
            navigate(-1)
        }
    }, [isSuccess])

    return (
        <div className="p-3">
            <div className="p-4 bg-white rounded-lg">
                <h1 className="text-lg font-bold">Nova transação</h1>
                <form method="post" className="flex flex-col space-y-2" onSubmit={handleSubmit}>
                    <Input label="Descrição" placeholder="Digite a descrição" value={description} onChange={(event) => setDescription(event.target.value)} />
                    <Input label="Valor" type="number" placeholder="0,00" value={amount} onChange={(event) => setAmount(Number(event.target.value))} />
                    <Select label="Tipo" onChange={(event) => setType(Number(event.target.value))} options={[
                        { value: 0, label: "Receita" },
                        { value: 1, label: "Despesa" }
                    ]} />
                    <div className="flex gap-2">
                        <div className="border border-gray-400 rounded px-3 py-2 cursor-pointer hover:bg-gray-100 flex-1" onClick={() => setModalType("person")}>
                            {
                                !person ? (
                                    <>
                                        <h2 className="text-sm font-bold">Pessoa</h2>
                                        <p className="text-xs text-gray-400">Selecione uma pessoa</p>
                                    </>
                                ) : <>
                                    <h2 className="text-sm font-bold">{person.name}</h2>
                                    <p className="text-xs text-gray-400">Pessoa selecionada</p>
                                </>
                            }
                        </div>
                        <div className="border border-gray-400 rounded px-3 py-2 cursor-pointer hover:bg-gray-100 flex-1" onClick={() => setModalType("category")}>
                            {
                                !category ? (
                                    <>
                                        <h2 className="text-sm font-bold">Categoria</h2>
                                        <p className="text-xs text-gray-400">Selecione uma categoria</p>
                                    </>
                                ) : <>
                                    <h2 className="text-sm font-bold">{category.description}</h2>
                                    <p className="text-xs text-gray-400">Categoria selecionada</p>
                                </>
                            }
                        </div>
                    </div>
                    <Modal isOpen={modalType == "person"} onClose={() => setModalType(null)} title="Pessoas">
                        {isPendingPerson && <p>Carregando...</p>}
                        <div className="p-2">
                            <table className="w-full text-sm mb-2">
                                <thead className="border-b border-gray-300 text-start text-gray-400">
                                    <tr>
                                        <th className="text-start uppercase py-4 pl-2">Nome</th>
                                        <th className="text-start uppercase">Idade</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {personData?.data?.map((person) => (
                                        <tr key={person.id} className="border-b border-gray-300 hover:bg-gray-100" onClick={() => { setPerson(person); setModalType(null); }}>
                                            <td className="py-4 pl-2">{person.name}</td>
                                            <td>{person.age}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                            <Pagination selectedPage={pagePerson} totalPages={personData?.totalPages ?? 0} fnChangePage={setPagePerson} />
                        </div>
                    </Modal>
                    <Modal isOpen={modalType == "category"} onClose={() => setModalType(null)} title="Categorias">
                        {isPendingCategory && <p>Carregando...</p>}
                        <div className="p-2">
                            <table className="w-full text-sm mb-2">
                                <thead className="border-b border-gray-300 text-start text-gray-400">
                                    <tr>
                                        <th className="text-start uppercase py-4 pl-2">Nome</th>
                                        <th className="text-start uppercase">Idade</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {categoryData?.data?.map((category) => (
                                        <tr key={category.id} className="border-b border-gray-300 hover:bg-gray-100" onClick={() => { setCategory(category); setModalType(null); }}>
                                            <td className="py-4 pl-2">{category.description}</td>
                                            <td>{types[category.type]}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                            <Pagination selectedPage={pageCategory} totalPages={categoryData?.totalPages ?? 0} fnChangePage={setPageCategory} />
                        </div>
                    </Modal>
                    {isError && <p>Erro ao adicionar transação.</p>}
                    <Button>Adicionar</Button>
                </form>
            </div>
        </div>
    )
}