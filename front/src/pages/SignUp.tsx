import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Mail, Lock, User, Eye, EyeOff, Loader2 } from "lucide-react";
import { useTranslation } from "react-i18next";
import { apiFetch } from "../lib/api";

const api = (path: string) => `${import.meta.env.VITE_API_URL ?? ""}${path}`;

const SignUp: React.FC = () => {
    const { t } = useTranslation();
    const [name, setName] = useState("");
    const [firstname, setFirstName] = useState("");
    const [email, setEmail] = useState("");
    const [pwd, setPwd] = useState("");
    const [pwd2, setPwd2] = useState("");
    const [showPwd, setShowPwd] = useState(false);
    const [loading, setLoading] = useState(false);
    const [err, setErr] = useState<string | null>(null);
    const navigate = useNavigate();

    async function onSubmit(e: React.FormEvent) {
        e.preventDefault();
        setErr(null);
        if (!name || !email || !pwd) {
            setErr("Tous les champs sont requis.");
            return;
        }
        if (pwd !== pwd2) {
            setErr("Les mots de passe ne correspondent pas.");
            return;
        }
        setLoading(true);
        try {
            const res = await apiFetch("/api/auth/sign-up", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ 
                    firstname: firstname,
                    lastname: name, 
                    email: email, 
                    password:pwd }),
            });
            if (!res.ok) {
                const txt = await res.text().catch(() => "");
                throw new Error(txt || `${res.status}`);
            }
            try {
                const data = await res.json();
                if (data?.token) localStorage.setItem("token", data.token);
                if (data?.user) localStorage.setItem("user", JSON.stringify(data.user));
            } catch {
            }
            window.dispatchEvent(new Event("auth-changed"));
            navigate("/");
        } catch (e: any) {
            setErr(e.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <main className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-zinc-900 px-4">
            <div className="w-full max-w-md bg-white dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 rounded-2xl p-6 shadow-sm">
                <h1 className="text-2xl font-semibold mb-1 text-zinc-900 dark:text-zinc-100">Créer un compte</h1>

                {err && (
                    <div className="mb-4 rounded-lg border border-red-300/60 bg-red-50 px-3 py-2 text-sm text-red-700 dark:border-red-800 dark:bg-red-900/30 dark:text-red-300">
                        {err}
                    </div>
                )}

                <form onSubmit={onSubmit} className="space-y-4">
                    <label className="block">
                        <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">Nom</span>
                        <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
                            <User className="h-4 w-4 text-zinc-400" aria-hidden />
                            <input
                                type="text"
                                className="w-full bg-transparent py-2.5 outline-none"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                required
                            />
                        </div>
                    </label>

                    <label className="block">
                        <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">Prénom</span>
                        <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
                            <User className="h-4 w-4 text-zinc-400" area-hidden />
                            <input
                                type="text"
                                className="w-full bg-transparent py-2.5 outline-none"
                                value={firstname}
                                onChange={(e) => setFirstName(e.target.value)}
                                required
                            />
                        </div>
                    </label>

                    <label className="block">
                        <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">Email</span>
                        <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
                            <Mail className="h-4 w-4 text-zinc-400" aria-hidden />
                            <input
                                type="email"
                                autoComplete="email"
                                className="w-full bg-transparent py-2.5 outline-none"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </div>
                    </label>

                    <label className="block">
                        <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">Mot de passe</span>
                        <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
                            <Lock className="h-4 w-4 text-zinc-400" aria-hidden />
                            <input
                                type={showPwd ? "text" : "password"}
                                autoComplete="new-password"
                                className="w-full bg-transparent py-2.5 outline-none"
                                value={pwd}
                                onChange={(e) => setPwd(e.target.value)}
                                required
                            />
                            <button
                                type="button"
                                aria-label={showPwd ? "Masquer" : "Afficher"}
                                onClick={() => setShowPwd((v) => !v)}
                                className="p-1 text-zinc-500 hover:text-zinc-700 dark:text-zinc-400 dark:hover:text-zinc-200"
                            >
                                {showPwd ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}
                            </button>
                        </div>
                    </label>

                    <label className="block">
                        <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">Confirmer le mot de passe</span>
                        <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
                            <Lock className="h-4 w-4 text-zinc-400" aria-hidden />
                            <input
                                type={showPwd ? "text" : "password"}
                                autoComplete="new-password"
                                className="w-full bg-transparent py-2.5 outline-none"
                                value={pwd2}
                                onChange={(e) => setPwd2(e.target.value)}
                                required
                            />
                        </div>
                    </label>

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full inline-flex items-center justify-center gap-2 rounded-lg bg-blue-600 hover:bg-blue-700 text-white py-2.5 font-medium disabled:opacity-60"
                    >
                        {loading && <Loader2 className="h-4 w-4 animate-spin" />}
                        Créer le compte
                    </button>
                </form>

                <p className="mt-4 text-sm text-zinc-600 dark:text-zinc-300">
                    Déjà inscrit ?{" "}
                    <Link to="/sign-in" className="text-blue-600 dark:text-blue-400 hover:underline">
                        {t('Header.signin')}
                    </Link>
                </p>
            </div>
        </main>
    );
};

export default SignUp;
