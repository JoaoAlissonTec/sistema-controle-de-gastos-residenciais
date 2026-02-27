import { useState } from "react";
import usePersons from "../../hooks/usePersons"
import Pagination from "../../components/Pagination";

export default function Home() {
    const [page, setPage] = useState<number>(1)
    const { data, isPending, error } = usePersons(page);

    if (isPending) return <p>Loading...</p>;
    if (error) return <p>{error.message}</p>;

    return (
        <div>
            <ul>
                {data?.data?.map((person) => (
                    <li key={person.id}>{person.name}</li>
                ))}
            </ul>
            <Pagination selectedPage={page} totalPages={data?.totalPages ?? 0} fnChangePage={setPage}/>
        </div>
    );
}