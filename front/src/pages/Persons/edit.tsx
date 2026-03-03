import { useEffect, useState } from "react";
import type { Person } from "../../services/persons";
import { useNavigate, useParams } from "react-router";
import Input from "../../components/Input";
import Button from "../../components/Button";
import usePerson from "../../hooks/usePerson";
import useUpdatePerson from "../../hooks/useUpdatePerson";

export default function EditPerson() {

    const navigate = useNavigate();
    const params = useParams()

    const [name, setName] = useState<string | undefined>()
    const [age, setAge] = useState<number>()
    const {mutate, isPending, isSuccess, isError} = useUpdatePerson()
    const {data, isPending: isPendingPerson, error} = usePerson(params.id ?? "")

    function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        if(isPending) return;
        if(!name || !age) return;

        const editedPerson: Person = {
            id: params.id,
            name,
            age
        }

        mutate(editedPerson);
    }

    useEffect(() => {
        document.title = 'Editar pessoa';
    }, [])

    useEffect(() => {
        if(isSuccess) {
            navigate("/persons")
        }
    }, [isSuccess])
    
    useEffect(() => {
        if (data) {
            setName(data.name);
            setAge(data.age);
        }
    }, [data]);

    if(isPendingPerson) return <p>Carregando...</p>
    if(error) return <p>{error.message}</p>

    return (
        <div className="p-3">
            <div className="bg-white p-4 rounded-lg">
                <h1 className="text-lg font-bold">Editar {data?.name}</h1>
                <form method="post" onSubmit={handleSubmit} className="flex flex-col space-y-2">
                    <Input label="Nome" placeholder="Digite o nome" value={name ?? ""} onChange={(event) => setName(event.target.value)}/>
                    <Input label="Idade" placeholder="Digite a idade" type="number" value={age} onChange={(event) => setAge(Number(event.target.value))}/>
                    <Button type="submit" variant="primary" isLoading={isPending}>Salvar</Button>
                </form>
                {isError && <p>Erro ao salvar pessoa.</p>}
            </div>
        </div>
    )
}