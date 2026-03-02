import React from "react";

interface Option {
  label: string;
  value: string | number;
}

interface SelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label?: string;
  error?: string;
  options: Option[];
}

export default function Select ({
  label,
  error,
  options,
  className,
  ...rest
}: SelectProps) {
  return (
    <div className="flex flex-col gap-1">
      {label && (
        <label className="text-sm font-medium">
          {label}
        </label>
      )}

      <select
        {...rest}
        className={`py-2 px-3 rounded border border-gray-400 text-sm outline-none ${className}`}
      >
        <option value="">Selecione...</option>

        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>

      {error && (
        <span className="text-red-500 text-xs">
          {error}
        </span>
      )}
    </div>
  );
};