export default function formatRelativeDate(date) {
    const newDate = new Date(date);

    const seconds = Math.floor((Date.now() - newDate) / 1000);

    const locale = navigator.language;
    const formatter = new Intl.RelativeTimeFormat(locale, { numeric: "always" });

    if (seconds < 60) {
        return formatter.format(-seconds, "second");
    }

    const minutes = Math.floor(seconds / 60);

    if (minutes < 60) {
        return formatter.format(-minutes, "minute");
    }

    const hours = Math.floor(minutes / 60);

    if (hours < 24) {
        return formatter.format(-hours, "hour");
    }

    const days = Math.floor(hours / 24);

    if (days < 7) {
        return formatter.format(-days, "day");
    }

    return newDate.toLocaleDateString();
}