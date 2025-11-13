import { apiFetch } from "../lib/api";
import React, { useState } from "react";
import { useTranslation } from "react-i18next";

const Home: React.FC = () => {
    const { t } = useTranslation();
  const [apiState, setApiState] = useState<"idle" | "loading" | "success" | "error">("idle");
  const [apiStatus, setApiStatus] = useState<number | null>(null);
  const [apiBody, setApiBody] = useState<string>("");

  const testApi = async () => {
    setApiState("loading");
    setApiStatus(null);
    setApiBody("");
    try {
      const res = await apiFetch("/api/buisness", { method: "GET", cache: "no-store" });
      setApiStatus(res.status);
      const text = await res.text();
      setApiBody(() => {
        try {
          return JSON.stringify(JSON.parse(text), null, 2);
        } catch {
          return text;
        }
      });
      setApiState(res.ok ? "success" : "error");
    } catch (e: any) {
      setApiState("error");
      setApiBody(String(e?.message || e));
    }
  };

  return (
    <section className="min-h-screen bg-gray-50 dark:bg-zinc-900 text-gray-900 dark:text-gray-100">
      <div className="container mx-auto px-4 py-20 lg:py-32">
        <div className="text-center">
          <h1 className="text-4xl sm:text-5xl lg:text-6xl font-bold mb-4">
            {t('Home.title')} <span className="text-blue-600 dark:text-blue-400">Romain</span>
          </h1>
          <p className="text-lg sm:text-xl max-w-2xl mx-auto mb-8">
            Développeur full-stack passionné par la création d'applications web modernes avec React, .NET et SQL Server.
          </p>
          <a
            href="/projects"
            className="inline-block px-6 py-3 text-white bg-blue-600 hover:bg-blue-700 rounded-md text-lg transition"
          >
            Voir mes projets
          </a>
        </div>

        <div className="mt-20 grid grid-cols-1 md:grid-cols-2 gap-12 items-center">
          <div>
            <h2 className="text-3xl font-semibold mb-4">À propos de moi</h2>
            <p className="mb-4">
              Je suis un développeur informatique basé à [Ville], spécialisé en développement web avec les technologies suivantes :
              <strong>React, TypeScript, .NET 8, SQL Server</strong>.
            </p>
            <p>
              J'aime travailler sur des projets où l'expérience utilisateur compte, optimiser les performances et explorer de nouvelles technologies.
            </p>
          </div>
        </div>

        <div className="mt-20">
          <h2 className="text-3xl font-semibold mb-8 text-center">Compétences</h2>
          <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-6">
            {[
              "React",
              "TypeScript",
              ".NET 8",
              "C#",
              "SQL Server",
              "Tailwind CSS",
              "Entity Framework",
              "Docker",
            ].map((skill) => (
              <div
                key={skill}
                className="bg-white dark:bg-zinc-800 p-6 rounded-lg text-center shadow hover:shadow-lg transition"
              >
                <p className="text-xl font-medium">{skill}</p>
              </div>
            ))}
          </div>
        </div>

        <div className="mt-20">
          <h2 className="text-3xl font-semibold mb-8 text-center">Projets récents</h2>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8">
            <div className="bg-white dark:bg-zinc-800 rounded-lg shadow-lg overflow-hidden hover:shadow-xl transition">
              <img src="/assets/project-image.png" alt="Aperçu projet" className="w-full h-48 object-cover" />
              <div className="p-6">
                <h3 className="text-2xl font-semibold mb-2">Nom du projet 1</h3>
                <p className="mb-4">
                  Courte description du projet, ce que j'ai réalisé, technologies utilisées.
                </p>
                <a href="/projects" className="text-blue-600 dark:text-blue-400 font-medium hover:underline">
                  Voir plus →
                </a>
              </div>
            </div>
          </div>
        </div>

        {/* Debug API */}
        <div className="mt-20">
          <h2 className="text-2xl font-semibold mb-4">Debug API</h2>
          <button
            onClick={testApi}
            className="px-4 py-2 rounded-md bg-zinc-900 text-white dark:bg-zinc-100 dark:text-zinc-900 hover:opacity-90 transition"
          >
            Tester /api/buisness
          </button>
          <div className="mt-4 text-sm">
            <div className="mb-2">
              État:{" "}
              <span
                className={
                  apiState === "loading"
                    ? "text-amber-600"
                    : apiState === "success"
                    ? "text-green-600"
                    : apiState === "error"
                    ? "text-red-600"
                    : "text-zinc-600"
                }
              >
                {apiState}
              </span>
              {apiStatus !== null && <span className="ml-2 text-zinc-500">HTTP {apiStatus}</span>}
            </div>
            {apiBody && (
              <pre className="p-3 bg-white dark:bg-zinc-800 rounded-md overflow-auto max-h-96 whitespace-pre-wrap wrap-break-words">
                {apiBody}
              </pre>
            )}
          </div>
        </div>
      </div>
    </section>
  );
};

export default Home;
