import { Mail, Phone } from "lucide-react";
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
                            <NavLink to={link.to} className={({ isActive }) => [
                                isActive ? "text-black font-medium" : "",
                                "hover:text-black"
                            ].join(" ")}>{link.label}</NavLink>
                        </li>
                    ))}
                </ul>
            </header>
            <section className="min-h-dvh bg-slate-100">
                <Outlet />
            </section>
            <footer className="bg-onyx text-white p-6 text-sm text-center">
                <div className="mb-4">
                    <h2 className="font-bold">Contatos</h2>
                    <div className="flex gap-5 justify-center">
                        <div className="flex items-center gap-2">
                            <Mail size={16} />
                            <a href="mailto:joaoalisson222005@gmail.com" className="underline">joaoalisson222005@gmail.com</a>
                        </div>
                        <span>|</span>
                        <div className="flex items-center gap-2">
                            <Phone size={16} />
                            <a href="tel:+5585982226635" className="underline">(85) 98222-6635</a>
                        </div>
                    </div>
                </div>
                <p className="text-xs text-center opacity-60">Teste técnico realizado por: João Alisson</p>
            </footer>
        </>
    )
}