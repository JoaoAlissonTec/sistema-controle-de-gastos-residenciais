import { NavLink, Outlet } from "react-router";

export default function Root() {

    const links = [
        {
            to: "/",
            label: "Início"
        },
        {
            to: "/transactions",
            label: "Transações"
        },
        {
            to: "/persons",
            label: "Pessoas"
        },
        {
            to: "/categories",
            label: "Categorias"
        }
    ]

    return (
        <>
            <header className="border-b border-gray-200 p-4 flex justify-between">
                <h1 className="font-bold uppercase">Controle de gastos residenciais</h1>
                <ul className="flex gap-5 text-sm text-gray-500">
                    {links.map((link) => (
                        <li id={link.label}>
                            <NavLink to={link.to} className={({isActive}) => [
                                isActive ? "text-black font-medium" : "",
                                "hover:text-black"
                            ].join(" ")}>{link.label}</NavLink>
                        </li>
                    ))}
                </ul>
            </header>
            <section className="min-h-dvh bg-slate-100">
                <Outlet/>
            </section>
            <footer className="bg-onyx text-white p-4">
                <span className="text-sm">João Alisson</span>
            </footer>
        </>
    )
}