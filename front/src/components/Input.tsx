interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label?: string;
    error?: string;
}

export default function Input({label, error, className, ...rest}: InputProps) {
    return (
        <div className="flex flex-col gap-1">
            {label && (
                <label className="text-sm font-medium">
                    {label}
                </label>
            )}

            <input 
                {...rest}
                className={`px-3 py-2 rounded border border-gray-400 outline-none text-sm ${className}`}
            />

            {error && (
                <span className="text-red-500 text-xs">
                    {error}
                </span>
            )}
        </div>
    ) 
}