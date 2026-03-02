import { useEffect, useState } from "react";
import type { Person } from "../../services/persons";
import useCreatePerson from "../../hooks/useCreatePerson";
import { useNavigate } from "react-router";
import Input from "../../components/Input";
import Button from "../../components/Button";

export default function NewPerson() {

    const navigate = useNavigate();
    const [name, setName] = useState<string | undefined>()
    const [age, setAge] = useState<number>()
    const {mutate, isPending, isSuccess, isError} = useCreatePerson()

    function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        if(isPending) return;
        if(!name || !age) return;

        const newPerson: Person = {
            name,
            age
        }

        mutate(newPerson);
    }

    useEffect(() => {
        if(isSuccess) {
            navigate("/persons")
        }
    }, [isSuccess])

    return (
        <div className="p-4">
            <h1 className="text-lg font-bold">Nova pessoa</h1>
            <form method="post" onSubmit={handleSubmit} className="flex flex-col space-y-2">
                <Input label="Nome" placeholder="Digite o nome" onChange={(event) => setName(event.target.value)}/>
                <Input label="Idade" placeholder="Digite a idade" type="number" onChange={(event) => setAge(Number(event.target.value))}/>
                <Button type="submit" variant="primary" isLoading={isPending}>Cadastrar</Button>
            </form>
            {isError && <p>Erro ao cadastrar nova pessoa.</p>}
        </div>
    )
}