import type { LucideProps } from "lucide-react"

type CardType = {
    icon: React.ForwardRefExoticComponent<Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>>,
    label: string,
    value: string
}

export default function Card({icon: Icon, label, value}: CardType) {
    return <div className="border border-gray-200 rounded-lg p-4 flex items-center gap-3 flex-1">
        <div className="bg-onyx rounded-lg p-2">
            <Icon className="text-white"/>
        </div>
        <div>
            <p className="font-bold">{value}</p>
            <p className="text-gray-400 text-sm">{label}</p>
        </div>
    </div>
}