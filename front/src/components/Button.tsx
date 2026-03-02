type ButtonVariant = "primary" | "secondary" | "danger";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    variant?: ButtonVariant;
    isLoading?: boolean;
}

export default function Button({
    variant = "primary",
    isLoading = false,
    children,
    disabled,
    ...rest
}: ButtonProps) {
    const getVariantStyle = (): React.CSSProperties => {
    switch (variant) {
      case "secondary":
        return {
          backgroundColor: "#e5e7eb",
          color: "#111827",
        };
      case "danger":
        return {
          backgroundColor: "red",
          color: "#ffffff",
        };
      default:
        return {
          backgroundColor: "black",
          color: "#ffffff",
        };
    }
  };

    return (
        <button
            {...rest}
            className="py-2 px-3 rounded border-none text-sm"
            style={{
                cursor: disabled || isLoading ? "not-allowed" : "pointer",
                opacity: disabled ? 0.6 : 1,
                ...getVariantStyle(),
            }}
        >
            {isLoading ? "Carregando..." : children}
        </button>
    )
}