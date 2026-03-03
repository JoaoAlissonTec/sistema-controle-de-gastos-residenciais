import { createBrowserRouter } from "react-router";
import Root from "./pages/Root";
import Home from "./pages/Home";
import Persons from "./pages/Persons";
import Transactions from "./pages/Transactions";
import Categories from "./pages/Categories";
import PersonTransactions from "./pages/Persons/personTransactions";
import NewPerson from "./pages/Persons/new";
import NewCategory from "./pages/Categories/new";
import CategoryTransactions from "./pages/Categories/categoryTransactions";
import EditPerson from "./pages/Persons/edit";
import NewTransaction from "./pages/Transactions/new";

export const router = createBrowserRouter([
    {
        path: "/",
        Component: Root,
        children: [
            {index: true, Component: Home},
            {path: "persons", Component: Persons},
            {path: "persons/new", Component: NewPerson},
            {path: "persons/:id", Component: PersonTransactions},
            {path: "persons/edit/:id", Component: EditPerson},
            {path: "transactions", Component: Transactions},
            {path: "transactions/new", Component: NewTransaction},
            {path: "categories", Component: Categories},
            {path: "categories/new", Component: NewCategory},
            {path: "categories/:id", Component: CategoryTransactions}
        ]
    }
])