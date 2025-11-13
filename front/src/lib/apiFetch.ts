import i18next from "i18next";

const getLang = () =>
  (i18next.language || localStorage.getItem("i18nextLng") || navigator.language || "fr")
    .split("-")[0]
    .toLowerCase();

export async function apiFetch(path: string, options: RequestInit = {}): Promise<Response> {
  const lang = getLang();
  const baseHeaders: Record<string, string> = { "Accept-Language": lang };
  const headers = { ...baseHeaders, ...(options.headers as any) };

  const res = await fetch(path, { ...options, headers });
  if (!res.ok) {
    const raw = await res.text().catch(() => "");
    let msg = raw;
    try { msg = (JSON.parse(raw)?.message as string) || raw; } catch {}
    throw new Error(msg || `HTTP ${res.status}`);
  }
  return res;
}
