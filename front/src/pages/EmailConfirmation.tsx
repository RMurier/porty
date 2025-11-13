import { useEffect, useMemo, useState } from "react";
import { useSearchParams, Link, useNavigate } from "react-router-dom";
import { CheckCircle2, XCircle, Loader2 } from "lucide-react";
import { useTranslation } from "react-i18next";
import { apiFetch } from "../lib/api";

type State = "idle" | "loading" | "success" | "error";

export default function EmailConfirmation() {
  const { t } = useTranslation();
  const [params] = useSearchParams();
  const navigate = useNavigate();

  const email = (params.get("email") || "").trim();
  const token = (params.get("token") || "").trim();

  const [state, setState] = useState<State>("idle");
  const [message, setMessage] = useState<string | null>(null);

  const canSubmit = useMemo(() => !!email && !!token, [email, token]);

  async function confirm() {
    if (!canSubmit) {
      setState("error");
      setMessage(t("emailConfirm.missingParams"));
      return;
    }
    setState("loading");
    setMessage(null);
    try {
      const res = await apiFetch("/api/auth/confirm-email", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, token }),
      });

      if (!res.ok) {
        const txt = await res.text().catch(() => "");
        throw new Error(txt || `HTTP ${res.status}`);
      }

      try {
        const data = await res.json();
        if (data?.message) setMessage(String(data.message));
      } catch {
        /* pas de JSON → ignorer */
      }

      setState("success");
    } catch (e: any) {
      setState("error");
      setMessage(e?.message || t("errors.generic"));
    }
  }

  useEffect(() => {
    confirm();
  }, [email, token]);

  return (
    <main className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-zinc-900 px-4">
      <div className="w-full max-w-md bg-white dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 rounded-2xl p-6 shadow-sm">
        {state === "loading" && (
          <div className="flex flex-col items-center text-center">
            <Loader2 className="h-8 w-8 animate-spin text-zinc-500" aria-hidden />
            <h1 className="mt-3 text-xl font-semibold text-zinc-900 dark:text-zinc-100">
              {t("emailConfirm.titleLoading")}
            </h1>
            <p className="mt-2 text-sm text-zinc-600 dark:text-zinc-300">
              {t("emailConfirm.subtitle")}
            </p>
          </div>
        )}

        {state === "success" && (
          <div className="text-center">
            <CheckCircle2 className="h-10 w-10 text-emerald-600 mx-auto" aria-hidden />
            <h1 className="mt-3 text-2xl font-semibold text-zinc-900 dark:text-zinc-100">
              {t("emailConfirm.titleSuccess")}
            </h1>
            <p className="mt-2 text-sm text-zinc-600 dark:text-zinc-300">
              {t("emailConfirm.success")}
            </p>
            {message && (
              <p className="mt-2 text-sm text-zinc-500 dark:text-zinc-400">{String(message)}</p>
            )}
            <div className="mt-6 flex items-center justify-center gap-3">
              <Link
                to="/sign-in"
                className="inline-flex items-center justify-center rounded-lg bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 font-medium"
              >
                {t("Header.signin")}
              </Link>
              <button
                type="button"
                onClick={() => navigate("/")}
                className="inline-flex items-center justify-center rounded-lg border border-zinc-300 dark:border-zinc-700 px-4 py-2 font-medium text-zinc-800 dark:text-zinc-100 hover:bg-zinc-50 dark:hover:bg-zinc-800"
              >
                {t("Header.categories.home")}
              </button>
            </div>
          </div>
        )}

        {state === "error" && (
          <div className="text-center">
            <XCircle className="h-10 w-10 text-red-600 mx-auto" aria-hidden />
            <h1 className="mt-3 text-2xl font-semibold text-zinc-900 dark:text-zinc-100">
              {t("emailConfirm.titleError")}
            </h1>
            <p className="mt-2 text-sm text-zinc-600 dark:text-zinc-300">
              {t("emailConfirm.error")}
            </p>
            {message && (
              <p className="mt-2 text-sm text-red-600 dark:text-red-400">{String(message)}</p>
            )}
            <div className="mt-6 flex items-center justify-center gap-3">
              <button
                type="button"
                onClick={confirm}
                className="inline-flex items-center justify-center rounded-lg bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 font-medium"
              >
                {t("form.retry")}
              </button>
              <Link
                to={`/sign-up?prefill=${encodeURIComponent(email)}`}
                className="inline-flex items-center justify-center rounded-lg border border-zinc-300 dark:border-zinc-700 px-4 py-2 font-medium text-zinc-800 dark:text-zinc-100 hover:bg-zinc-50 dark:hover:bg-zinc-800"
              >
                {t("signUp.title")}
              </Link>
            </div>
            <p className="mt-4 text-xs text-zinc-500 dark:text-zinc-400">
              {t("emailConfirm.note")}
            </p>
          </div>
        )}
      </div>
    </main>
  );
}
