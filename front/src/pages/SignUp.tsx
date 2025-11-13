import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Mail, Lock, User, Eye, EyeOff, Loader2, CheckCircle2, RefreshCw } from "lucide-react";
import { useTranslation } from "react-i18next";
import { apiFetch } from "../lib/apiFetch";

const SignUp: React.FC = () => {
  const { t } = useTranslation();

  const [lastName, setLastName] = useState("");
  const [firstName, setFirstName] = useState("");
  const [email, setEmail] = useState("");
  const [pwd, setPwd] = useState("");
  const [pwd2, setPwd2] = useState("");

  const [showPwd, setShowPwd] = useState(false);
  const [loading, setLoading] = useState(false);
  const [err, setErr] = useState<string | null>(null);

  // Panneau de confirmation inline après inscription
  const [confirmEmail, setConfirmEmail] = useState<string | null>(null);
  const [resendInfo, setResendInfo] = useState<string | null>(null);
  const [cooldown, setCooldown] = useState(0);

  useEffect(() => {
    if (cooldown <= 0) return;
    const id = setInterval(() => setCooldown((s) => s - 1), 1000);
    return () => clearInterval(id);
  }, [cooldown]);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();
    setErr(null);
    setResendInfo(null);

    if (!lastName || !firstName || !email || !pwd || !pwd2) {
      setErr(t("form.allRequired"));
      return;
    }
    if (pwd !== pwd2) {
      setErr(t("form.pwdMismatch"));
      return;
    }

    setLoading(true);
    try {
      const res = await apiFetch("/api/auth/sign-up", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          firstname: firstName,
          lastname: lastName,
          email,
          password: pwd,
        }),
      });

      if (!res.ok) {
        const txt = await res.text().catch(() => "");
        throw new Error(txt || `${res.status}`);
      }

      setConfirmEmail(email);
      setCooldown(60);
    } catch (e: any) {
      setErr(e?.message ?? t("errors.generic"));
    } finally {
      setLoading(false);
    }
  }

  async function resendEmail() {
    if (!confirmEmail) return;
    setResendInfo(null);
    try {
      const res = await apiFetch("/api/auth/resend-confirmation", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email: confirmEmail }),
      });
      if (!res.ok) {
        const txt = await res.text().catch(() => "");
        throw new Error(txt || `${res.status}`);
      }
      setResendInfo(t("signUp.resent"));
      setCooldown(60);
    } catch (e: any) {
      setResendInfo(e?.message ?? t("errors.generic"));
    }
  }

  if (confirmEmail) {
    return (
      <main className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-zinc-900 px-4">
        <div className="w-full max-w-md bg-white dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 rounded-2xl p-6 shadow-sm">
          <div className="flex items-start gap-3">
            <CheckCircle2 className="h-6 w-6 text-emerald-600 mt-1" aria-hidden />
            <div>
              <h1 className="text-2xl font-semibold text-zinc-900 dark:text-zinc-100">
                {t("signUp.checkTitle")}
              </h1>
              <p className="mt-2 text-sm text-zinc-600 dark:text-zinc-300">
                {t("signUp.checkDesc")}
              </p>
              <p className="mt-2 font-medium text-zinc-900 dark:text-zinc-100">{confirmEmail}</p>
            </div>
          </div>

          <div className="mt-6 flex items-center gap-3">
            <button
              onClick={resendEmail}
              disabled={cooldown > 0}
              className="inline-flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 px-3 py-2 text-sm text-zinc-800 dark:text-zinc-100 hover:bg-zinc-50 dark:hover:bg-zinc-800 disabled:opacity-60"
            >
              <RefreshCw className="h-4 w-4" aria-hidden />
              {cooldown > 0
                ? t("signUp.resendIn").replace("{{s}}", String(cooldown))
                : t("signUp.resent")}
            </button>

            {resendInfo && (
              <span className="text-sm text-zinc-600 dark:text-zinc-300">{resendInfo}</span>
            )}
          </div>

          <p className="mt-4 text-sm text-zinc-600 dark:text-zinc-300">
            {t("signUp.help")}
          </p>

          <p className="mt-6 text-sm text-zinc-600 dark:text-zinc-300">
            {t("signUp.backToLoginHint")}{" "}
            <Link to="/sign-in" className="text-blue-600 dark:text-blue-400 hover:underline">
              {t("Header.signin")}
            </Link>
          </p>
        </div>
      </main>
    );
  }

  return (
    <main className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-zinc-900 px-4">
      <div className="w-full max-w-md bg-white dark:bg-zinc-900 border border-zinc-200 dark:border-zinc-800 rounded-2xl p-6 shadow-sm">
        <h1 className="text-2xl font-semibold mb-1 text-zinc-900 dark:text-zinc-100">
          {t("signUp.title")}
        </h1>

        {err && (
          <div className="mb-4 rounded-lg border border-red-300/60 bg-red-50 px-3 py-2 text-sm text-red-700 dark:border-red-800 dark:bg-red-900/30 dark:text-red-300">
            {err}
          </div>
        )}

        <form onSubmit={onSubmit} className="space-y-4">
          <label className="block">
            <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">
              {t("form.lastname")}
            </span>
            <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
              <User className="h-4 w-4 text-zinc-400" aria-hidden />
              <input
                type="text"
                className="w-full bg-transparent py-2.5 outline-none"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                required
              />
            </div>
          </label>

          <label className="block">
            <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">
              {t("form.firstname")}
            </span>
            <div className="flex items-center gap-2 rounded-lg border border-zinc-300 dark:border-zinc-700 bg-white dark:bg-zinc-950 px-3">
              <User className="h-4 w-4 text-zinc-400" aria-hidden />
              <input
                type="text"
                className="w-full bg-transparent py-2.5 outline-none"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                required
              />
            </div>
          </label>

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
                autoComplete="new-password"
                className="w-full bg-transparent py-2.5 outline-none"
                value={pwd}
                onChange={(e) => setPwd(e.target.value)}
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

          <label className="block">
            <span className="mb-1 block text-sm text-zinc-600 dark:text-zinc-300">
              {t("form.passwordConfirm")}
            </span>
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
            {loading && <Loader2 className="h-4 w-4 animate-spin" aria-hidden />}
            {t("signUp.createAccount")}
          </button>
        </form>

        <p className="mt-4 text-sm text-zinc-600 dark:text-zinc-300">
          {t("signUp.alreadyRegistred")}{" "}
          <Link to="/sign-in" className="text-blue-600 dark:text-blue-400 hover:underline">
            {t("Header.signin")}
          </Link>
        </p>
      </div>
    </main>
  );
};

export default SignUp;
