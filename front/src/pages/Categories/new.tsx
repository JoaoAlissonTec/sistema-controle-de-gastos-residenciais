import { useNavigate } from "react-router";
import type { Category } from "../../services/categories";
import { useEffect, useState } from "react";
import Input from "../../components/Input";
import Button from "../../components/Button";
import useCreateCategory from "../../hooks/useCreateCategory";
import Select from "../../components/Select";

export default function NewCategory() {
    const navigate = useNavigate();
    const [description, setDescription] = useState<string | undefined>()
    const [type, setType] = useState<number>(0)
    const {mutate, isPending, isSuccess, isError} = useCreateCategory()

    function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        if(isPending) return;
        console.log(!description || type < 0)
        if(!description || type < 0) return;

        const newCategory: Category = {
            description,
            type
        }

        mutate(newCategory);
    }

    useEffect(() => {
        if(isSuccess) {
            navigate("/categories")
        }
    }, [isSuccess])

    return (
        <div className="p-4">
            <h1 className="text-lg font-bold">Nova categoria</h1>
            <form method="post" onSubmit={handleSubmit} className="flex flex-col space-y-2">
                <Input label="Descrição" placeholder="Digite a descrição" onChange={(event) => setDescription(event.target.value)}/>
                <Select onChange={(event) => setType(Number(event.target.value))} options={[
                    {value: 0, label: "Receita"},
                    {value: 1, label: "Despesa"},
                    {value: 2, label: "Ambas"},
                ]}/>
                <Button type="submit" variant="primary" isLoading={isPending}>Cadastrar</Button>
            </form>
            {isError && <p>Erro ao cadastrar nova pessoa.</p>}
        </div>
    )
}