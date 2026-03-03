import { useEffect } from "react"

export default function Home() {
    useEffect(() => {
        document.title = 'Início';
    }, [])

    return (
        <div className="h-dvh flex flex-col justify-center items-start p-4">
            <h1 className="text-2xl font-black uppercase">Gerencie seus gastos residenciais</h1>
            <p>Conheça o poder do gerenciamento e mantenha o controle sobre os gastos da sua casa.</p>
        </div>
    )
}