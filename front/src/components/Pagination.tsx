import type { Dispatch, SetStateAction } from "react"

type PaginationType = {
    selectedPage: number,
    totalPages: number,
    fnChangePage: Dispatch<SetStateAction<number>>
}

export default function Pagination({ selectedPage, totalPages, fnChangePage }: PaginationType) {
    const maxButtons = 10
    const half = Math.floor(maxButtons / 2)

    const start = Math.max(1, selectedPage - half)
    const end = Math.min(totalPages, selectedPage + half)

    if (totalPages < 2) return;
    return (
        <div className="space-x-1">
            <button
                onClick={() => { fnChangePage(selectedPage - 1) }}
                className="border border-transparent px-2 rounded cursor-pointer hover:bg-gray-200 disabled:bg-white disabled:text-gray-400"
                disabled={selectedPage == 1}>Previous</button>

            {Array.from({ length: end - start + 1 }, (_, i) => {
                const page = start + i

                return (
                    <button
                        key={page}
                        onClick={() => fnChangePage(page)}
                        className={`border border-transparent px-2 rounded cursor-pointer hover:bg-gray-200 ${selectedPage === page ? "bg-gray-200 border-gray-400" : ""
                            }`}
                    >
                        {page}
                    </button>
                )
            })}

            <button
                onClick={() => { fnChangePage(selectedPage + 1) }}
                className="border border-transparent px-2 rounded cursor-pointer hover:bg-gray-200 disabled:bg-white disabled:text-gray-400"
                disabled={selectedPage == totalPages}>Next</button>
        </div>
    )
}