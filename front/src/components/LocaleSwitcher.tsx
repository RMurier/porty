import { useTranslation } from 'react-i18next';

const languages = ['fr', 'en', 'es'] as const;

export default function LocaleSwitcher() {
  const { i18n } = useTranslation();
  return (
    <div className="flex gap-2 p-3">
      {languages.map(l => (
        <button
          key={l}
          onClick={() => i18n.changeLanguage(l)}
          aria-current={i18n.language.startsWith(l) || undefined}
        >
          {l.toUpperCase()}
        </button>
      ))}
    </div>
  );
}