
type HeaderProps = {
  onMenuClick: () => void;
  title: string;
};

export default function Header({
  onMenuClick,
  title,
}: HeaderProps) {
  return (
    <header className="app-header">
      <h1>{title}</h1>

      <button
        className="menu-button"
        onClick={onMenuClick}
        aria-label="Abrir menu de navegação"
        aria-haspopup="true"
      >
        <span />
        <span />
        <span />
      </button>
    </header>
  );
}
