import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';

import fr from './locales/fr/common.json';
import en from './locales/en/common.json';

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources: { fr: { common: fr }, en: { common: en } },
    fallbackLng: 'en',
    supportedLngs: ['fr', 'en'],
    load: 'languageOnly',
    nonExplicitSupportedLngs: true,
    ns: ['common'],
    defaultNS: 'common',
    interpolation: { escapeValue: false },
    detection: {
      order: ['querystring', 'localStorage', 'cookie', 'navigator', 'htmlTag'],
      lookupQuerystring: 'lang',
      caches: ['localStorage', 'cookie']
    },
    debug: false
  });

export default i18n;
