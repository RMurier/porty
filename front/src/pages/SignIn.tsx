import React, { useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { Mail, Lock, Eye, EyeOff, Loader2 } from "lucide-react";
import { useTranslation } from "react-i18next";
import { apiFetch } from "../lib/apiFetch";
import { useAuthStore, AuthUser } from "../stores/authStore";

const SignIn: React.FC = () => {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPwd, setShowPwd] = useState(false);
  const [loading, setLoading] = useState(false);
  const [err, setErr] = useState<string | null>(null);

  const navigate = useNavigate();
  const location = useLocation();
  const from = (location.state as any)?.from ?? "/";

  const setAuth = useAuthStore((state) => state.setAuth);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();
    setErr(null);

    if (!email || !password) {
      setErr(t("form.allRequired"));
      return;
    }

    setLoading(true);
    try {
      const res = await apiFetch("/api/auth/sign-in", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });
      console.log(res)
      console.log(1)

      if (!res.ok) {
      console.log(2)
        const txt = await res.text().catch(() => "");
      console.log(3)
        throw new Error(txt || t("errors.generic"));
      }
      console.log(4)

      const data = await res.json();
      console.log(5)

      const accessToken: string | undefined = data?.accessToken;
      console.log(6)
      const refreshToken: string | undefined = data?.refreshToken;
      console.log(7)

      if (!accessToken || !refreshToken) {
      console.log(8)
        throw new Error(t("errors.generic"));
      }

      const user: AuthUser = {
        id: data.id,
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        isEmailValidated: data.isEmailValidated,
        createdAt: data.createdAt,
      };

      setAuth({
        user,
        accessToken,
        refreshToken,
      });

      navigate(from, { replace: true });
    } catch (e: any) {
      setErr(e?.message || t("errors.generic"));
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-zinc-900 px-4">
      <div className="w-full max-w-md bg-white dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 rounded-2xl p-6 shadow-sm">
        <h1 className="text-2xl font-semibold mb-1 text-zinc-900 dark:text-zinc-100">
          {t("signIn.title")}
        </h1>

        {err && (
          <div className="mb-4 rounded-lg border border-red-300/60 bg-red-50 px-3 py-2 text-sm text-red-700 dark:border-red-800 dark:bg-red-900/30 dark:text-red-300">
            {err}
          </div>
        )}

        <form onSubmit={onSubmit} className="space-y-4">
          <label className="block">
            <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">
              {t("form.email")}
            </span>
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
            <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">
              {t("form.password")}
            </span>
            <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
              <Lock className="h-4 w-4 text-zinc-400" aria-hidden />
              <input
                type={showPwd ? "text" : "password"}
                autoComplete="current-password"
                className="w-full bg-transparent py-2.5 outline-none"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
              <button
                type="button"
                aria-label={showPwd ? t("form.hide") : t("form.show")}
                onClick={() => setShowPwd((v) => !v)}
                className="p-1 text-zinc-500 hover:text-zinc-700 dark:text-zinc-400 dark:hover:text-zinc-200"
              >
                {showPwd ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}
              </button>
            </div>
          </label>

          <button
            type="submit"
            disabled={loading}
            className="w-full inline-flex items-center justify-center gap-2 rounded-lg bg-blue-600 hover:bg-blue-700 text-white py-2.5 font-medium disabled:opacity-60"
          >
            {loading && <Loader2 className="h-4 w-4 animate-spin" aria-hidden />}
            {t("Header.signin")}
          </button>
        </form>

        <p className="mt-4 text-sm text-zinc-600 dark:text-zinc-300">
          {t("signIn.noAccount")}{" "}
          <Link
            to="/sign-up"
            className="text-blue-600 dark:text-blue-400 hover:underline"
          >
            {t("signIn.createAccount")}
          </Link>
        </p>
      </div>
    </main>
  );
};

export default SignIn;
